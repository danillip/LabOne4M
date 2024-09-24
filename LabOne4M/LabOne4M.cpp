#include <iostream>
#include <fstream>
#include <vector>
#include <sstream>

void printMatrix(const std::vector<std::vector<double>>& matrix) {
    for (const auto& row : matrix) {
        for (double val : row) {
            std::cout << val << " ";
        }
        std::cout << std::endl;
    }
}

void readData(const std::string& filename, std::vector<std::vector<double>>& matrix, std::vector<double>& f) {
    std::ifstream file(filename);
    std::string line;
    bool readingMatrix = true;

    while (std::getline(file, line)) {
        if (line.empty()) {
            readingMatrix = false; // Пустая строка
            continue;
        }

        if (readingMatrix) {
            std::istringstream iss(line);
            std::vector<double> row;
            double value;
            while (iss >> value) {
                row.push_back(value);
            }
            matrix.push_back(row);
        }
        else {
            // Чтение вектора f
            std::istringstream iss(line);
            double value;
            while (iss >> value) {
                f.push_back(value);
            }
        }
    }
}

void transformToIdentity(std::vector<std::vector<double>>& matrix, std::vector<double>& f) {
    size_t n = matrix.size();
    std::vector<double> a(n), b(n), c(n), p(n), q(n); // Объявление векторов
    size_t step = 0;

    // Основной цикл преобразования
    for (size_t i = 0; i < n; ++i) {
        double pivot = matrix[i][i];

        // Проверка на деление на ноль
        if (pivot == 0) {
            std::cerr << "Ошибка: нулевой элемент на главной диагонали" << std::endl;
            return;
        }

        // Делим все элементы строки на элемент-пивот
        for (size_t j = 0; j < n; ++j) {
            matrix[i][j] /= pivot; // Приведение к единичному виду
        }
        f[i] /= pivot;

        // Обновляем векторы a, b, c для текущей строки
        a[i] = (i > 0) ? matrix[i][i - 1] : 0; // Элементы верхней диагонали
        b[i] = (i < n - 1) ? matrix[i][i + 1] : 0; // Элементы нижней диагонали
        c[i] = matrix[i][i]; // Главная диагональ

        // Вывод промежуточного результата
        std::cout << "Шаг " << ++step << ": Приведение строки " << i + 1 << " к единичному виду." << std::endl;
        printMatrix(matrix);
        std::cout << "Вектор f: ";
        for (double val : f) {
            std::cout << val << " ";
        }
        std::cout << std::endl;

        // Обнуление других строк
        for (size_t j = 0; j < n; ++j) {
            if (j != i) {
                double factor = matrix[j][i]; // Определяем множитель
                std::cout << "Обнуляем элемент в строке " << j + 1 << " с помощью строки " << i + 1 << "." << std::endl;
                for (size_t k = 0; k < n; ++k) {
                    matrix[j][k] -= factor * matrix[i][k]; // Вычитаем из строки j
                }
                f[j] -= factor * f[i]; // Обновляем вектор f

                // Выводим математическую формулу для вычитания
                std::cout << "Формула: строка " << j + 1 << " = строка " << j + 1 << " - (" << factor << " * строка " << i + 1 << ")." << std::endl;
            }
        }
    }

    // Проверка и вывод окончательных значений
    std::cout << "Шаг " << ++step << ": Проверка окончательных значений матрицы и вектора f." << std::endl;
    printMatrix(matrix);
    std::cout << "Финальный вектор f: ";
    for (double val : f) {
        std::cout << val << " ";
    }
    std::cout << std::endl;
}

int main() {
    setlocale(LC_ALL, "Russian");
    std::vector<std::vector<double>> matrix;
    std::vector<double> f;

    readData("Data.txt", matrix, f);

    // Шаг 0: вывод считанной матрицы и вектора f
    std::cout << "Шаг 0: Считанная матрица:" << std::endl;
    printMatrix(matrix);
    std::cout << "Считанный вектор f: ";
    for (double val : f) {
        std::cout << val << " ";
    }
    std::cout << std::endl;

    // Начинаем преобразование матрицы к единичному виду
    transformToIdentity(matrix, f);

    return 0;
}
