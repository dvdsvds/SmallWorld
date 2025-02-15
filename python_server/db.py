import mysql.connector
from mysql.connector import Error, pooling
import bcrypt

def cp():
    return mysql.connector.pooling.MySQLConnectionPool(
        pool_name="mypool",
        pool_size=10,
        host='211.197.219.43',
        user='yeon',
        password='0735',
        database='Chat'
    )

def gc():
    pool = cp()
    conn = pool.get_connection()
    return conn


def gun(username):
    conn = gc()  # 커넥션 가져오기
    if conn is not None:
        cursor = conn.cursor()
        cursor.execute("select id, username, password from user where username = %s", (username,))
        user = cursor.fetchall()
        cursor.close()  # 커서 닫기
        conn.close()  # 커넥션 반환
        return user
    return None

def iu(username, password):
    conn = gc()  # 커넥션 가져오기
    if conn is not None:
        cursor = conn.cursor()
        hspwd = bcrypt.hashpw(password.encode('utf-8'), bcrypt.gensalt())
        cursor.execute("insert into user (username, password) values (%s, %s)", (username, hspwd))
        conn.commit()  # 변경 사항 커밋
        cursor.close()  # 커서 닫기
        conn.close()  # 커넥션 반환
