import pandas as pd
import matplotlib.pyplot as plt
from sklearn import linear_model
import numpy as np
from sklearn.preprocessing import StandardScaler

def getBatches(x, y, batch_size):
  n = x.shape[0] # num of rows
  indecies = np.arange(n) # array from 0 to length with step 1
  np.random.shuffle(indecies) # shuffle for random values in single batch
  batches = []
  for start in range(0, n, batch_size):
    end = min(start + batch_size, n) # index of the last fitted batch
    batch_indecies = indecies[start:end] # indices of fitted batches
    x_batch = x[batch_indecies] # batch features
    y_batch = y[batch_indecies] # batch values
    batches.append((x_batch, y_batch))
  return batches

def miniBatchGradientDescend(x, y, batch_size, iters, gD_step, L2_coeff):
    w = np.zeros((x.shape[1])) # num of columns
    b = 0
    rows = x.shape[0]
    for i in range(iters):
      batches = getBatches(x, y, batch_size)
      for x_batch, y_batch in batches:
        predicted_ys = x_batch @ w + b
        derivW = -(2/rows) * x_batch.T @ (y_batch - predicted_ys) + 2 * L2_coeff * w
        derivB = -(2/rows) * np.sum(y_batch - predicted_ys)
        w = w - gD_step * derivW
        b = b - gD_step * derivB
    return w, b

training_data = pd.read_excel('predict_house_price_training_data.xlsx')
training_data.head()

target_variable_name = 'Целевая.Цена'
training_values = training_data[target_variable_name]
training_points = training_data.drop(target_variable_name, axis=1)

linear_regression_model = linear_model.LinearRegression()
linear_regression_model.fit(training_points, training_values)

scaler = StandardScaler()
training_points_scaled = scaler.fit_transform(training_points)

w, b = miniBatchGradientDescend(training_points_scaled, training_values, 3000, 1000, 0.1, 0.01)
linear_regression_model.fit(training_points_scaled, training_values)
y_train_linear_predicted = linear_regression_model.predict(training_points_scaled)


