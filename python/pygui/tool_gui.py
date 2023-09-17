from PyQt5.QtWidgets import QMainWindow, QApplication, QPushButton, QLabel, QFileDialog
from PyQt5 import uic
from PyQt5.QtGui import QPixmap
import sys

class UI(QMainWindow):
    def __init__(self):
        super(UI, self).__init__()

        uic.loadUi("art_study_tool.ui", self)

        self.button = self.findChild(QPushButton, "pushButton")
        self.label = self.findChild(QLabel, "pushButton")
        self.button.clicked.connect(self.clicker)


        self.show()

    def clicker(self):
        fname = QFileDialog.getOpenFileName(self, "Open photo")




app = QApplication(sys.argv)
UIWindow = UI()
app.exec_()