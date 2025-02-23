from flask import Blueprint, jsonify, request
from db import get_user

login_bp = Blueprint('login', __name__)

# login API
@login_bp.route('/attempt', methods=['POST'])
def login():
    data = request.get_json()
    
    if data is None:
        print("Error : 'data' is None")
        return jsonify({"status": "error", "message": "invalid request"}), 400

    email = data["email"]
    password = data["password"]

    if not email or not password:
        return jsonify({"status": "error", "message": "missing credentials"}), 400

    user = get_user(email, password)
    
    if not user:
        return jsonify({"status": "error", "message": "invalid credentials"}), 401


    return jsonify({
        "status": "success",
        "message": "login successful",
    }), 200

