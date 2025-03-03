import React, { useEffect, useState } from "react";
import { getUsers, deleteUser } from "../services/api";

const UserList = () => {
  const [users, setUsers] = useState([]);

  useEffect(() => {
    fetchUsers();
  }, []);

  const fetchUsers = async () => {
    try {
      const response = await getUsers();
      const data = response.data;
      setUsers(Array.isArray(data) ? data : []); // Ensure users is always an array
    } catch (error) {
      console.error("Error fetching users:", error);
      setUsers([]); // Prevents map() crash
    }
  };

  const handleDelete = async (id) => {
    await deleteUser(id);
    fetchUsers();
  };

  return (
    <div>
      <h2>Users</h2>
      <ul>
        {users.map(user => (
          <li key={user.id}>
            {user.name} ({user.email}) 
            <button onClick={() => handleDelete(user.id)}>Delete</button>
          </li>
        ))}
      </ul>
    </div>
  );
};

export default UserList;