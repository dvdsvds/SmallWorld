import mysql.connector
import bcrypt
from dotenv import load_dotenv
import os

load_dotenv(dotenv_path='../config/s.env')

def create_pool():
    return mysql.connector.pooling.MySQLConnectionPool(
        pool_name="mypool",
        pool_size=10,
        host=os.getenv('db_host'),
        user=os.getenv('db_user'),
        password=os.getenv('db_password'),
        database=os.getenv('db_name')
    )

def get_connection():
    pool = create_pool()
    conn = pool.get_connection()
    return conn

def get_user_name(username):
    conn = get_connection()   
    if conn is not None:
        cursor = conn.cursor()
        cursor.execute("select username from user where username = %s", (username,))
        user = cursor.fetchall()
        cursor.close()  
        conn.close()  
        return user
    return None

def insert_user(username, email, hashed_password):
    conn = get_connection()
    if conn is not None:
        cursor = conn.cursor()
        try:
            cursor.execute("INSERT INTO user (username, email, password) VALUES (%s, %s, %s)", 
                           (username, email, hashed_password))
            conn.commit()
            return True  # 데이터베이스에 삽입 성공
        except Exception as e:
            print(f"Error inserting user: {e}")
            conn.rollback()
            return False  # 삽입 실패
        finally:
            cursor.close()
            conn.close()
    return False  # 연결 실패


def get_code(email, code):
    conn = get_connection()
    if conn is not None:
        cursor = conn.cursor()
        cursor.execute("select email, code from email_code where email = %s and code = %s", (email, code))
        row = cursor.fetchone()

        if row:
            cursor.execute("delete from email_code where email = %s", (email,))
            conn.commit()
            cursor.close()
            conn.close()
            return row[1]
        else:
            cursor.close()
            conn.close()
            return None
    return None
