import React, { useEffect, useState } from "react";
import { getTasks, deleteTask } from "../services/api";

const TaskList = () => {
  const [tasks, setTasks] = useState([]);

  useEffect(() => {
    fetchTasks();
  }, []);

  const fetchTasks = async () => {
    try {
      const response = await getTasks();
      const data = response.data;
      setTasks(Array.isArray(data) ? data : []); // Ensure tasks is always an array
    } catch (error) {
      console.error("Error fetching tasks:", error);
      setTasks([]); // Prevent map() crash
    }
  };

  const handleDelete = async (id) => {
    await deleteTask(id);
    fetchTasks();
  };

  return (
    <div>
      <h2>Tasks</h2>
      {tasks.length === 0 ? (
        <p>Loading tasks...</p>
      ) : (
        <ul>
          {tasks.map(task => (
            <li key={task.id}>
              {task.title} - {task.status}
              <button onClick={() => handleDelete(task.id)}>Delete</button>
            </li>
          ))}
        </ul>
      )}
    </div>
  );
}
export default TaskList;