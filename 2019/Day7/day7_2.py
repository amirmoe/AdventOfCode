import sys
from itertools import permutations 

class Amplifier_memory:
  def __init__(self, instructions: list, pointer: int, phase_setting: int):
    self.instructions = instructions
    self.pointer = pointer
    self.phase_setting = phase_setting
    self.use_phase = True
    self.input_signal = 0
    self.halted = False

def get_operation(number: int) -> int:
  number_string = str(number)
  last_two = number_string[-2:]
  return int(last_two)

def immediate_mode(number: int) -> tuple:
  number_string = str(number)
  first_parameter = False
  second_parameter = False
  if (len(number_string) < 3 ):
    return (first_parameter, second_parameter)

  if int(number_string[-3]) == 1:
    first_parameter = True
  if len(number_string) > 3:  
    if int(number_string[-4]) == 1:
      second_parameter = True
  return (first_parameter, second_parameter)

def get_value(array: list, pointer: int, immediate: bool) -> int:
  value = array[pointer]
  if not immediate:
    value = array[value]
  return value

def amplifier(amp: Amplifier_memory, input_signal: int):
  array = amp.instructions
  pointer = amp.pointer

  while array[pointer] != 99:
    operation = get_operation(array[pointer])
    immediate = immediate_mode(array[pointer])
    
    if operation == 1:
      array[array[pointer+3]] = get_value(array, pointer+1, immediate[0]) + get_value(array, pointer+2, immediate[1]) 
      pointer+=4

    elif operation == 2:
      array[array[pointer+3]] = get_value(array, pointer+1, immediate[0]) * get_value(array, pointer+2, immediate[1]) 
      pointer+=4

    elif operation == 3:
      if amp.use_phase:
        inp = amp.phase_setting
        amp.use_phase = False
      else:
        inp = input_signal
      array[array[pointer+1]] = int(inp)
      pointer+=2

    elif operation == 4:
      amp.input_signal = get_value(array, pointer+1, immediate[0])
      pointer+=2
      amp.pointer = pointer
      break

    elif operation == 5:
      value = get_value(array, pointer+1, immediate[0])
      if value != 0:
        pointer = get_value(array, pointer+2, immediate[1])
      else:
        pointer+=3

    elif operation == 6:
      value = get_value(array, pointer+1, immediate[0])
      if value == 0:
        pointer = get_value(array, pointer+2, immediate[1])
      else:
        pointer+=3

    elif operation == 7:
      first_value = get_value(array, pointer+1, immediate[0])
      second_value = get_value(array, pointer+2, immediate[1])
      array[array[pointer+3]] = 1 if first_value < second_value else 0
      pointer+=4

    elif operation == 8:
      first_value = get_value(array, pointer+1, immediate[0])
      second_value = get_value(array, pointer+2, immediate[1])
      array[array[pointer+3]] = 1 if first_value == second_value else 0
      pointer+=4
    else:
      print("Something went wrong!")

  if array[pointer] == 99:
    amp.halted = True

  return amp


inp = sys.stdin.readline()
inp = inp.strip()
string_list = inp.split(',')
instructions = list(map(int, string_list))

outputs = []
perm = permutations([9,8,7,6,5])   
for i in list(perm): 
  amplifier_setting = i
  memory = [
    Amplifier_memory(instructions.copy(), 0, amplifier_setting[0]),
    Amplifier_memory(instructions.copy(), 0, amplifier_setting[1]) , 
    Amplifier_memory(instructions.copy(), 0, amplifier_setting[2]), 
    Amplifier_memory(instructions.copy(), 0, amplifier_setting[3]), 
    Amplifier_memory(instructions.copy(), 0, amplifier_setting[4]) 
  ]

  while memory[4].halted != True:
    for i in range (5):
      amp = amplifier(memory[i], memory[i-1].input_signal)
      memory[i] = amp
      
  outputs.append(memory[4].input_signal)

outputs.append(memory[4].input_signal)
outputs.sort()
print(outputs[len(outputs)-1])


