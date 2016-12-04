# task1.py
#
# Example interactions with the Create
#
# you can load this file the first time with
# >>> import task1
# and you can reload it after making changes with
# >>> reload(task1)
# you can import all of the names into the local namespace with
# >>> from task1 import *
#
# Or, you can simply copy this line every time:
# >>> import task1 ; reload(task1) ; from task1 import *

import create
import time


def quickleft(r):
		#does a quick turn then stops
		#+ is CCW, - is CW
	r.Go( 0, 60)
	time.sleep(.1)
	r.stop()

def quickright(r):
		#does a quick turn then stops
		#+ is CCW, - is CW
	r.Go( 0, -60)
	time.sleep(.1)
	r.stop()	

def forward (r):
	
	r.Go(30, 0)
	time.sleep(1)
	r.stop()
	
	

def longturn(r):
		#does a quick turn then stops
		#+ is CCW, - is CW
	r.Go( 0, 60)
	time.sleep(3)
	r.stop()
	
def aboutface(r):
		#does a quick turn then stops
		#+ is CCW, - is CW
	r.Go( 0, 60)
	time.sleep(3)
	r.stop()
	

#
# using a function that takes the robot as input...
#
def backup(r):
    """ this function simply backs the robot up
        for two seconds (as an aside, this string is
        printed if you type help(task1.backup)
        
        the input is the robot, r
    """
    r.go(-50,0)
    time.sleep(.5) # go for .5 seconds
    r.stop();
    

#
# another function example...
#
def testLoop(r):
    """ an example of an sense-plan-action loop
        admittedly, there's not much planning here
        
        the input is the robot, r
    """
    r.toSafeMode()
    while True:
        
        # sense
        d = r.sensors()  # get all sensors...
        
        # "plan"
        left = d[create.LEFT_BUMP]
        right = d[create.RIGHT_BUMP]
        playButton = d[create.PLAY_BUTTON]
        
        # act
        if right > 0:
            print 'right bump'
            r.go(0,0)
        elif left > 0:
            print 'left bump'
            r.go(0,30)
        elif playButton > 0:
            print 'button press'
            r.go()
            break
        
        # don't go too fast...
        time.sleep(0.015)
    
    print 'End of testLoop\'s loop!'

#
# an example of deriving a class...
#
class MyCreate(create.Create):
    
    def __init__(self):
        """ this constructor has a hard-coded value for
            the serial port to ease typing
        """
        serialportname = '/dev/tty.RooTooth-COM0-1'
        create.Create.__init__(self,serialportname)
    
    def beethoven(self):
        """ plays four famous notes """
        self.playSong( [(55,16),(55,16),(55,16),(51,64)] )
            
        
    
    
