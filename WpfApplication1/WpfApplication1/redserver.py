#!/usr/bin/python           # This is server.py file

import socket               # Import socket module
import create
import task1

from task1 import *

b = create.Create(7)



s = socket.socket(socket.AF_INET, socket.SOCK_STREAM)         # Create a socket object
host = socket.gethostname() # Get local machine name
port = 12347                # Reserve a port for your service.
BUFFER_SIZE = 1024 			# Normally 1024, but we want fast response
s.bind((host, port))        # Bind to the port

while True:
	s.listen(5)

	conn, addr = s.accept()
	print 'Connection address:', addr
	while True:
		print "top"
		data = conn.recv(BUFFER_SIZE)
		print "bottom"
		conn.send(data)  # echo
	
		if data == 'r':
			print "received data:", data
			quickright(b)	

		elif data == 'l':
			print "received data:", data
			quickleft(b)	
	
		elif data == 'a':
			print "received data:", data
			aboutface(b)	

		elif data == 'f':
			print "received data:", data
			forward(b)					

		data = 0
		
		conn.close()
		break
	
		

