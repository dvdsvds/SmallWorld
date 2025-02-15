from flask import Flask, jsonify, request
from db import gun  # db.py에서 gun 함수 가져오기
import bcrypt

app = Flask(__name__)

# 로그인 API
@app.route('/login', methods=['POST'])
def login():
    data = request.json
    username = data['username']
    password = data['password']

    # DB에서 사용자 정보 조회
    user = gun(username)

    # 사용자 정보가 없을 경우 처리
    if user is None or len(user) == 0:
        return jsonify({"status": "error", "message": "사용자가 존재하지 않습니다."}), 404

    stored_pwd = user[0][2]  # 비밀번호는 3번째 인덱스 (id, username, password)
    
    # 비밀번호 확인
    if bcrypt.checkpw(password.encode('utf-8'), stored_pwd.encode('utf-8')):
        return jsonify({"status": "success", "message": "로그인 성공"}), 200
    else:
        return jsonify({"status": "error", "message": "비밀번호가 틀립니다."}), 401

# 회원가입 API
@app.route('/register', methods=['POST'])
def register():
    data = request.json
    username = data['username']
    password = data['password']

    # DB에 사용자 정보 삽입
    from db import iu  # db.py에서 iu 함수 가져오기
    iu(username, password)

    return jsonify({"status": "success", "message": "회원가입 성공"}), 201

if __name__ == '__main__':
    app.run(host='0.0.0.0', port=5000)


