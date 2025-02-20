import bcrypt
from flask import Flask, jsonify, request
from db import get_user_name, get_code
from cEmail import send_code_email

app = Flask(__name__)

# login API
@app.route('/login', methods=['POST'])
def login():
    data = request.json
    
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

    stored_pwd = user[0][1]  
    
    # 비밀번호 확인
    if bcrypt.checkpw(password.encode('utf-8'), stored_pwd.encode('utf-8')):
        return jsonify({"status": "success", "message": "login successful"}), 200
    else:
        return jsonify({"status": "error", "message": "invalid password"}), 401

# username check API
@app.route('/register/check-username', methods=['POST'])
def check_username():
    data = request.get_json()
    username = data['username']

    if not username:
        return jsonify({"status": "error", "message": "user required"}), 200

    user = get_user_name(username)

    if user is None or len(user) == 0:
        return jsonify({"status": "success", "message": "available"}), 200
    else:
        return jsonify({"status": "error", "message": "taken"}), 400

# send verification code API
@app.route('/register/send-email', methods=['POST'])
def send_code():
    data = request.get_json()
    email = data['email']

    if not email:
        return jsonify({"status": "error", "message": "please enter an email"}), 400

    result = send_code_email(email)
    if result:
        return jsonify({"status": "success", "message": "send code to email"}), 200
    else:
        return jsonify({"status": "error", "message": "failed to send email"}), 500

# check verification code API
@app.route('/register/send-email/check-code', methods=['POST'])
def check_code():
    data = request.get_json()
    print(f"recv : {data}")
    email = data["email"]
    code = data["code"]

    if not email or not code:
        return jsonify({"status": "error", "message": "code is missing from data"}), 400

    stored_code = get_code(email, code)
    if stored_code is None:
        return jsonify({"status": "error", "message": "code not found or code expired"}), 404

    if code != stored_code:
        return jsonify({"status": "error", "message": "invalid code"}), 401

    return jsonify({"status": "success", "message": "verified"}), 200

if __name__ == '__main__':
    app.run(host='0.0.0.0', port=5000)


