import React from "react";
import UserList from "./components/UserList";
import UserForm from "./components/UserForm";
import TaskList from "./components/TaskList";
import TaskForm from "./components/TaskForm";

const App = () => {
  return (
    <div>
      <h1>To-Do App</h1>
      <UserForm refreshUsers={() => window.location.reload()} />
      <UserList />
      <TaskForm refreshTasks={() => window.location.reload()} />
      <TaskList />
    </div>
  );
};

export default App;