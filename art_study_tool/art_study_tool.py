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
        error_mes()
    else:
        return path_list


def fix_position(img):
    scale_width = GetSystemMetrics(0) / img.shape[1]
    scale_height = GetSystemMetrics(1) / img.shape[0]
    scale = min(scale_width, scale_height)
    width = img.shape[1] * scale
    height = img.shape[0] * scale
    x_gap = GetSystemMetrics(0) - width
    y_gap = GetSystemMetrics(1) - height
    # print(
    #     GetSystemMetrics(0), GetSystemMetrics(1), 
    #     img.shape[1], img.shape[0], 
    #     width, height,
    #     x_gap, y_gap
    # )
    return [width, height, x_gap, y_gap]


def show_img(path_list, loop, seconds):
    for x in range(loop):
        try:
            img = cv2.imread(random.choice(path_list))
        except:
            error_mes()
        else:
            screen = fix_position(img)
            cv2.namedWindow("Display", cv2.WINDOW_NORMAL)
            cv2.resizeWindow("Display", int(screen[0] ), int(screen[1]) )
            cv2.imshow("Display", img)
            cv2.moveWindow("Display", int(screen[2] / 2), int(screen[3] / 2) )
            # print(int(screen[2] / 2), int(screen[3] / 2))
            cv2.setWindowProperty("Display", cv2.WND_PROP_TOPMOST, 1)
            cv2.waitKey(1)
            time.sleep(seconds)
            cv2.destroyAllWindows()

def error_mes():
        print("------------------------------------\ninvalid input received.\n------------------------------------")

def main():
    print("------------------------------------\nArt study tool by Christopher Crispy\n------------------------------------")
    while True:
        try:
            my_path = input("Path to all the photo: ")
            my_loop = int(input("Amount of photo to display: "))
            my_time = int(input("Time interval for each photo in seconds: "))
        except:
            error_mes()
        else:
            my_list = load_dic(my_path)
            show_img(my_list, my_loop, my_time)
            print("-------------------------------\nCongrats, You finished a study!\n-------------------------------")
            

if __name__ == "__main__":
    main()
