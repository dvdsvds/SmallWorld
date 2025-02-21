from flask import Flask
from login import login_bp
from register import register_bp

app = Flask(__name__)

app.register_blueprint(login_bp, url_prefix='/login')
app.register_blueprint(register_bp, url_prefix='/register')

if __name__ == '__main__':
    app.run(host='0.0.0.0', port=5000)
