import React, { useState } from "react";
import { createUser } from "../services/api";

const UserForm = ({ refreshUsers }) => {
  const [user, setUser] = useState({ name: "", email: "", passwordHash: "" });

  const handleChange = (e) => {
    setUser({ ...user, [e.target.name]: e.target.value });
  };

  const handleSubmit = async (e) => {
    e.preventDefault();
    await createUser(user);
    refreshUsers();
  };

  return (
    <form onSubmit={handleSubmit}>
      <input type="text" name="name" placeholder="Name" onChange={handleChange} required />
      <input type="email" name="email" placeholder="Email" onChange={handleChange} required />
      <input type="password" name="passwordHash" placeholder="Password" onChange={handleChange} required />
      <button type="submit">Add User</button>
    </form>
  );
};

export default UserForm;