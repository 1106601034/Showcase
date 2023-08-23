# -*- coding: utf-8 -*-
import time
import cv2
import random
import os
from win32api import GetSystemMetrics


def load_dic(path):
    path_list = []
    try:
        for file in os.listdir(path):
            file_path = os.path.join(path, file)
            if os.path.isdir(file_path):
                pass
            else:
                path_list.append(file_path)
    except:
        print("invalid path")
    else:
        return path_list


def fix_position(img):
    scale_width = GetSystemMetrics(0) / img.shape[1]
    scale_height = GetSystemMetrics(1) / img.shape[0]
    scale = min(scale_width, scale_height)
    width = int(img.shape[1]) * scale
    height = int(img.shape[0]) * scale
    x_gap = (GetSystemMetrics(0) - width) / 2
    y_gap = (GetSystemMetrics(1) - height) / 2
    print(int(x_gap), int(y_gap))
    return [int(width), int(height), int(x_gap), int(y_gap)]


def show_img(path_list, loop, seconds):
    for x in range(loop):
        try:
            img = cv2.imread(random.choice(path_list))
        except:
            print("invalid file.")
        else:
            screen = fix_position(img)
            cv2.namedWindow("Display", cv2.WINDOW_NORMAL)
            cv2.resizeWindow("Display", screen[0], screen[1])
            cv2.imshow("Display", img)
            cv2.moveWindow("Display", screen[2], screen[3])
            cv2.setWindowProperty("Display", cv2.WND_PROP_TOPMOST, 1)
            cv2.waitKey(1)
            time.sleep(seconds)
            cv2.destroyAllWindows()


def main():
    while True:
        try:
            my_time = int(input("seconds: "))
            my_path = input("path: ")
            my_loop = int(input("loop: "))
        except:
            print("invalid input.")
        else:
            my_list = load_dic(my_path)
            show_img(my_list, my_loop, my_time)


if __name__ == "__main__":
    main()
