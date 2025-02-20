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
        cursor.execute("select id, username from user where username = %s", (username,))
        user = cursor.fetchall()
        cursor.close()  
        conn.close()  
        return user
    return None

def insert_user(username, password):
    conn = get_connection()
    if conn is not None:
        cursor = conn.cursor()
        hspwd = bcrypt.hashpw(password.encode('utf-8'), bcrypt.gensalt())
        cursor.execute("insert into user (username, password) values (%s, %s)", (username, hspwd))
        conn.commit()
        cursor.close()
        conn.close()

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
