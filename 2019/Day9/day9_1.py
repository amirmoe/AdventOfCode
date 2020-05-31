import sys
from itertools import permutations 

class Intcode_computer:
  def __init__(self, instructions: list, input_signal: int):
    self.instructions = instructions
    self.input_signal = input_signal
    self.pointer = 0
    self.output_signal = 0
    self.relative_base = 0
    self.halted = False

  def get_operation(self) -> int:
    number_string = str(self.instructions[self.pointer])
    last_two = number_string[-2:]
    return int(last_two)

  def get_mode(self) -> tuple:
    number_string = str(self.instructions[self.pointer])
    number_string = str(number_string).zfill(5)

    return (int(number_string[2]), int(number_string[1]), int(number_string[0]))

  def get_value(self, mode: int, parameter: int) -> int:
    if mode == 0:
      return self.instructions[self.instructions[self.pointer+parameter]]
    if mode == 1:
      return self.instructions[self.pointer+parameter]
    if mode == 2:
      return self.instructions[self.instructions[self.pointer+parameter]+self.relative_base]

  def set_value(self, mode: int, parameter: int, value: int):
    if mode == 0:
      self.instructions[self.instructions[self.pointer+parameter]] = value
    if mode == 1:
      self.instructions[self.pointer+parameter] = value
    if mode == 2:
      self.instructions[self.instructions[self.pointer+parameter]+self.relative_base] = value

  def run_computer(self):
    while self.instructions[self.pointer] != 99:
      operation = self.get_operation()
      mode = self.get_mode()
      #print (f"Preforming operation {operation} with mode {mode} on pointer {self.pointer}")
      
      if operation == 1:
        value = self.get_value(mode[0],1) + self.get_value(mode[1],2) 
        self.set_value(mode[2], 3, value)
        self.pointer+=4

      elif operation == 2:
        value = self.get_value(mode[0],1) * self.get_value(mode[1],2) 
        self.set_value(mode[2], 3, value)
        self.pointer+=4

      elif operation == 3:
        self.set_value(mode[0], 1, int(self.input_signal))
        self.pointer+=2

      elif operation == 4:
        self.output_signal = self.get_value(mode[0],1)
        self.pointer+=2
        print(f"Output:{self.output_signal}")
        #break

      elif operation == 5:
        value = self.get_value(mode[0],1)
        if value != 0:
          self.pointer = self.get_value(mode[1],2) 
        else:
          self.pointer+=3

      elif operation == 6:
        value = self.get_value(mode[0],1)
        if value == 0:
          self.pointer = self.get_value(mode[1],2) 
        else:
          self.pointer+=3

      elif operation == 7:
        first_value = self.get_value(mode[0],1)
        second_value = self.get_value(mode[1],2)   
        self.set_value(mode[2], 3, 1 if first_value < second_value else 0)
        self.pointer+=4

      elif operation == 8:
        first_value = self.get_value(mode[0],1)
        second_value = self.get_value(mode[1],2)
        self.set_value(mode[2], 3, 1 if first_value == second_value else 0)
        self.pointer+=4

      elif operation == 9:
        self.relative_base = self.relative_base+self.get_value(mode[0],1)
        self.pointer+=2

      else:
        print("Something went wrong!")

    if self.instructions[self.pointer] == 99:
      self.halted = True


inp = sys.stdin.readline()
inp = inp.strip()
string_list = inp.split(',')
instructions = list(map(int, string_list))
instructions.extend([0] * 100000)

computer = Intcode_computer(instructions, 2)
computer.run_computer()
