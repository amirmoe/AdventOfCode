import sys

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

def main():
  inp = sys.stdin.readline()
  inp = inp.strip()
  string_list = inp.split(',')
  array = list(map(int, string_list))
  
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
      inp = input("Enter input\n")
      array[array[pointer+1]] = int(inp)
      pointer+=2

    elif operation == 4:
      print ("Operation 4: " + str(get_value(array, pointer+1, immediate[0])))
      pointer+=2

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
    
    

main()

