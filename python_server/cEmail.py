import smtplib
import random
import string
import datetime
import time
import os
from db import get_connection
from dotenv import load_dotenv
from email.mime.text import MIMEText
from email.mime.multipart import MIMEMultipart

load_dotenv(dotenv_path='../config/s.env')

def send_email(to_email, code):
    sender = os.getenv('email_sender')
    password = os.getenv('email_password')

    if not sender or not password:
        return ValueError("환경 변수 'email_sender' 또는 'email_password'가 설정되지 않았습니다")
    subject = "인증 코드"

    body = f"귀하의 인증 코드: {code}\n코드는 5분 동안 유효합니다."

    msg = MIMEMultipart()
    msg['From'] = sender
    msg['To'] = to_email
    msg['Subject'] = subject
    msg.attach(MIMEText(body, 'plain'))

    try:
        server = smtplib.SMTP('smtp.gmail.com', 587)
        server.starttls()
        server.login(sender, password)

        server.sendmail(sender, to_email, msg.as_string())
        server.quit()
        print(f"인증 코드({code})가 {to_email}로 발송되었습니다.")
    except Exception as e:
        print(f"이메일 발송 실패: {e}")
        raise

def create_code():
    return ''.join(random.choices(string.digits, k=6))

def save_code(email, code):
    conn = get_connection()  
    if conn is not None:
        cursor = conn.cursor()
        expiration_time = time.time() + 300
        expiration_datetime = datetime.datetime.fromtimestamp(expiration_time, tz=datetime.timezone.utc).strftime('%Y-%m-%d %H:%M:%S')
        cursor.execute("""
            INSERT INTO email_code (email, code, expiration_time)
            VALUES (%s, %s, %s)
        """, (email, code, expiration_datetime))
        conn.commit()
        cursor.close()
        conn.close()
        print("인증 코드가 DB에 저장되었습니다.")
    
def send_code_email(email):
    code = create_code()
    send_email(email, code)
    print(f"{email}로 인증 코드가 발송되었습니다.")
    save_code(email, code)
    return True
