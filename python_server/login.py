import bcrypt
from flask import Blueprint, jsonify, request
from db import get_user_name

login_bp = Blueprint('login', __name__)

# login API
@login_bp.route('/login', methods=['POST'])
def login():
    data = request.get_json()
    
    if data is None:
        print("Error : 'data' is None")
        return jsonify({"status": "error", "message": "invalid request"}), 400

    username = data['username']
    password = data['password']
    user = get_user_name(username)

    if not username or not password:
        return jsonify({"status": "error", "message": "missing credentials"}), 400
        
    if user is None or len(user) == 0:
        return jsonify({"status": "error", "message": "user does not exist"}), 404

    stored_pwd = user[0][2]  
    
    # 비밀번호 확인
    if bcrypt.checkpw(password.encode('utf-8'), stored_pwd.encode('utf-8')):
        return jsonify({"status": "success", "message": "login successful"}), 200
    else:
        return jsonify({"status": "error", "message": "invalid password"}), 401
