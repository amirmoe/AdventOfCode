import sys
from itertools import permutations 


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

def amplifier(array: list, phase_setting: int, input_signal: int) -> int:
  phase = True
  
  pointer = 0
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
      if phase:
        inp = phase_setting
        phase = False
      else:
        inp = input_signal
      array[array[pointer+1]] = int(inp)
      pointer+=2

    elif operation == 4:
      return get_value(array, pointer+1, immediate[0])

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


inp = sys.stdin.readline()
inp = inp.strip()
string_list = inp.split(',')
instructions = list(map(int, string_list))

perm = permutations([0,1,2,3,4]) 
  
outputs = []
for i in list(perm): 
  amplifier_setting = i
  amplifier_a = amplifier(instructions, amplifier_setting[0],0)
  amplifier_b = amplifier(instructions, amplifier_setting[1],amplifier_a)
  amplifier_c = amplifier(instructions, amplifier_setting[2],amplifier_b)
  amplifier_d = amplifier(instructions, amplifier_setting[3],amplifier_c)
  amplifier_e = amplifier(instructions, amplifier_setting[4],amplifier_d)
  outputs.append(amplifier_e)

outputs.sort()
print(outputs[len(outputs)-1])


