# -*- coding: utf-8 -*-
import os
import sys
import tensorflow as tf
from tensorflow import keras
from tensorflow.keras.models import Sequential
from tensorflow.keras.layers import Dense
import numpy as np

# TensorFlowのログレベルを設定
os.environ['TF_CPP_MIN_LOG_LEVEL'] = '3'  # 0 = すべてのログ, 1 = INFOログを抑制, 2 = WARNINGログを抑制, 3 = ERRORログを抑制

def create_model(action_count):
    model = Sequential()
    model.add(Dense(24, activation='relu', input_shape=(action_count,)))
    model.add(Dense(24, activation='relu'))
    model.add(Dense(action_count, activation='linear'))
    model.compile(optimizer='adam', loss='mse')
    return model

def save_model(model, file_path):
    model.save(file_path)

def load_model(file_path):
    return keras.models.load_model(file_path)

def predict(model, state):
    # 進行状況メッセージを抑制
    with tf.device('/cpu:0'):
        return model.predict(state, verbose=0)

def train_on_batch(model, state, target):
    model.train_on_batch(state, target)

def main():
    if len(sys.argv) < 2:
        print("No function name provided")
        return

    function_name = sys.argv[1]

    try:
        if function_name == "initialize_model":
            action_count = int(sys.argv[2])
            model = create_model(action_count)
            save_model(model, "model.keras")
        elif function_name == "save_model":
            model = load_model("model.keras")
            save_model(model, sys.argv[2])
        elif function_name == "load_model":
            model = load_model(sys.argv[2])
        elif function_name == "choose_action":
            model = load_model("model.keras")
            state = np.array([float(x) for x in sys.argv[2].split(",")])
            action = np.argmax(predict(model, np.array([state]))[0])
            print(action)  # 必要な情報だけを出力
        elif function_name == "update_model":
            model = load_model("model.keras")
            state = np.array([float(x) for x in sys.argv[2].split(",")])
            action = int(sys.argv[3])
            reward = float(sys.argv[4])
            new_state = np.array([float(x) for x in sys.argv[5].split(",")])
            target = predict(model, np.array([state]))[0]
            target[action] = reward + 0.9 * np.max(predict(model, np.array([new_state]))[0])
            train_on_batch(model, np.array([state]), np.array([target]))
            save_model(model, "model.keras")
    except Exception as e:
        print(f"Error: {e}")

if __name__ == "__main__":
    main()
