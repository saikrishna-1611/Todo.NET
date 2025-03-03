import axios from "axios";

const API_URL = "http://localhost:5004/api";

export const getUsers = () => axios.get(`${API_URL}/Users`);
export const createUser = (user) => axios.post(`${API_URL}/Users`, user);
export const deleteUser = (id) => axios.delete(`${API_URL}/Users/${id}`);

export const getTasks = () => axios.get(`${API_URL}/TaskItems`);
export const createTask = (task) => axios.post(`${API_URL}/TaskItems`, task);
export const deleteTask = (id) => axios.delete(`${API_URL}/TaskItems/${id}`);