import mysql.connector
import logging
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

def get_user(email, password):
    conn = get_connection()
    if conn is None:
        logging.error("Database connection failed")
        return None

    try:
        with conn.cursor() as cursor:
            cursor.execute("SELECT id, email, password FROM user WHERE email = %s", (email,))
            row = cursor.fetchone()
            if row:
                stored_password = row[2]  # 데이터베이스에서 가져온 해시된 비밀번호
                if isinstance(stored_password, str):
                    stored_password = stored_password.encode("utf-8")  # 문자열이면 바이트로 변환

                if bcrypt.checkpw(password.encode("utf-8"), stored_password):
                    return {"id": row[0], "email": row[1]}
            return None
    except Exception as e:
        logging.error(f"Database error: {e}")
        return None
    finally:
        conn.close()

