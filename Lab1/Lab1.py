import csv
import os
import numpy as np
from prettytable import PrettyTable

MAX_SIZE = 2**20
MAX_PERCENT = 101
PERCENT_STEP = 5


def check_file_type(file_path):
    if not file_path.endswith(".csv"):
        raise ValueError("Это не CSV файл!")
    else:
        return True


def check_file_size(file_path):
    size = os.path.getsize(file_path)
    if size == 0:
        raise ValueError("Файл пустой!")
    elif size > MAX_SIZE:
        raise ValueError("Файл слишком большой!")
    else:
        return True


def read_file(file_path):
    try:
        with open(file_path, "r", encoding="utf-8") as file:
            csv_reader = csv.reader(file, delimiter=",")
            data = []
            header = next(csv_reader)
            column_count = len(header)
            for row in csv_reader:
                if len(row) != column_count:
                    raise ValueError(
                        f"Ошибка в строке: {row}. Количество значений не совпадает с количеством столбцов."
                    )
                data.append(row)
            return data, header
    except FileNotFoundError:
        raise FileNotFoundError("Файл не существует!")
    except csv.Error as e:
        raise csv.Error(f"Ошибка чтения CSV файла: {e}")


def modify_data(data, region):
    return [row for row in data[1:] if row[1] == region]


def make_set_of_regions(data):
    return sorted(set(row[1] for row in data))


def calculate_percentile(data, percent):
    return np.percentile(data, percent)


def calculate_metrics(data, column_id):
    try:
        values = [float(row[column_id]) for row in data]
        if not values:
            raise ValueError()
        values.sort()
        minimum = values[0]
        maximum = values[-1]
        median = np.median(values)
        average = sum(values) / len(values)
        percentiles = {
            i: calculate_percentile(values, i)
            for i in range(0, MAX_PERCENT, PERCENT_STEP)
        }
        return [maximum, minimum, median, average, percentiles]
    except ValueError:
        raise ValueError(f"Ошибка при расчете метрик, некорректные данные")


def check_input(inp, amount):
    if inp.isnumeric():
        res = int(inp)
        if res <= 0 or res > amount:
            raise ValueError("Некорректный ввод. Введите номер из списка!")
        else:
            return res
    else:
        raise ValueError("Необходимо ввести числовое значение!")


def print_results(data, metrics, header):
    main_table = PrettyTable()
    main_table.field_names = header
    main_table.add_rows(data)
    print(main_table)
    if isinstance(metrics, list):
        print(
            "\nМаксимум: {}\nМинимум: {}\nМедиана: {}\nСреднее значение: {:.4f}\n".format(
                *metrics
            )
        )
        print("Таблица перцентилей для данного столбца")
        percent_table = PrettyTable()
        percent_table.field_names = ["Перцентиль", "Значение"]
        percent_table.add_rows(
            [f"{p}-й", round(value, 4)] for p, value in metrics[4].items()
        )
        print(percent_table)
    else:
        print(metrics)


def print_set(s):
    for index, value in enumerate(s, start=1):
        print(f"{index}: {value}")


file_path = input("Введите путь до файла (до 1 Мб): ")

try:
    if check_file_type(file_path):
        if check_file_size(file_path):
            data, header = read_file(file_path)
            set_of_regions = make_set_of_regions(data)
            print_set(set_of_regions)
            region = check_input(input("Введите номер региона: "), len(set_of_regions))
            print_set(header[2:])
            column_id = check_input(input("Введите ID столбца: "), len(header[2:]))
            data = modify_data(data, set_of_regions[region - 1])
            metrics = calculate_metrics(data, column_id + 1)
            print_results(data, metrics, header)
except (FileNotFoundError, csv.Error, ValueError) as e:
    print(f"Ошибка: {e}")
