import bcrypt
from flask import Blueprint, jsonify, request
from db import get_user_name, get_code, insert_user
from cEmail import send_code_email

register_bp = Blueprint('register', __name__)

# username check API
@register_bp.route('/check-username', methods=['POST'])
def check_username():
    data = request.get_json()
    username = data["username"]

    if not username:
        return jsonify({"status": "error", "message": "user required"}), 400

    user = get_user_name(username)

    if user is None or len(user) == 0:
        return jsonify({"status": "success", "message": "available"}), 200
    else:
        return jsonify({"status": "error", "message": "taken"}), 400

# send verification code API
@register_bp.route('/send-email', methods=['POST'])
def send_code():
    data = request.get_json()
    email = data["email"]

    if not email:
        return jsonify({"status": "error", "message": "please enter an email"}), 400

    result = send_code_email(email)
    if result:
        return jsonify({"status": "success", "message": "send code to email"}), 200
    else:
        return jsonify({"status": "error", "message": "failed to send email"}), 500

# check verification code API
@register_bp.route('/check-code', methods=['POST'])
def check_code():
    data = request.get_json()
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

# register API
@register_bp.route('/done', methods=['POST'])
def done_register():
    data = request.get_json()
    print(f"{data}")
    username = data.get("username")
    email = data.get("email")
    password = data.get("password")
    confirm_password = data.get("confirm_password")  # confirm_password 값을 받아옵니다.

    if not username:
        return jsonify({"status": "error", "message": "username field is required"}), 400
    elif not email:
        return jsonify({"status": "error", "message": "email field is required"}), 400
    elif not password:
        return jsonify({"status": "error", "message": "password field is required"}), 400
    elif not confirm_password:
        return jsonify({"status": "error", "message": "confirm_password field is required"}), 400

    # 비밀번호 확인
    if password != confirm_password:
        return jsonify({"status": "error", "message": "passwords do not match"}), 400

    # 비밀번호 해싱
    salt = bcrypt.gensalt()
    hashed_password = bcrypt.hashpw(password.encode('utf-8'), salt)


    # 데이터베이스에 해시된 비밀번호 저장
    result = insert_user(username, email, hashed_password.decode('utf-8'))

    if result:
        return jsonify({"status": "success", "message": "register done"}), 200
    else:
        return jsonify({"status": "error", "message": "register failed"}), 500

