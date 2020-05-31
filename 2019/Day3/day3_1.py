import sys


wire1_str = sys.stdin.readline()
wire1_str = wire1_str.strip()
wire1_arr = wire1_str.split(',')
wire2_str = sys.stdin.readline()
wire2_str = wire2_str.strip()
wire2_arr = wire2_str.split(',')
wires = [wire1_arr, wire2_arr]

wires_coordinates = []

for wire in range(len(wires)):
  pen_x = 0
  pen_y = 0

  wire_tuples = []

  for instruction in wires[wire]:
    direction = instruction[0]
    steps = int(instruction[1::])

    if (direction=='U'):
      for i in range(1,steps+1):
        wire_tuples.append((pen_x, pen_y-i))
      pen_y = pen_y-steps
    elif (direction=='D'):
      for i in range(1,steps+1):
        wire_tuples.append((pen_x, pen_y+i))
      pen_y = pen_y+steps
    elif (direction=='R'):
      for i in range(1,steps+1):
        wire_tuples.append((pen_x+i, pen_y))
      pen_x = pen_x+steps
    elif (direction=='L'):
      for i in range(1,steps+1):
        wire_tuples.append((pen_x-i, pen_y))
      pen_x = pen_x-steps
    else:
      print("something went wrong!")

  wires_coordinates.append(wire_tuples)


intersection = list(set(wires_coordinates[0]) & set(wires_coordinates[1])) 

distances = []
for coord in intersection:
  distances.append(abs(coord[0])+abs(coord[1]))
distances.sort()
print(distances[0])