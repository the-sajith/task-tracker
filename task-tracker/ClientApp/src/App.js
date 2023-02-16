import React, { Component } from "react";
import Header from "./components/Header";
import Tasks from "./components/Tasks";
import { useState, useEffect } from "react";
import AddTask from "./components/AddTask";
import Footer from "./components/Footer";

function App() {
  const [tasks, setTasks] = useState([]);

  const [showAddTask, setShowAddTask] = useState(false);

  // action works all the way up and state is passed way down

  const toggleReminder = async (id) => {
    const tasksToToggle = await fetchTask(id);
    const updateTask = { ...tasksToToggle, reminder: !tasksToToggle.reminder };

      const res = await fetch(`http://localhost:5278/tasks/${id}`, {
      method: "PUT",
      headers: {
        "Content-type": "application/json",
      },
      body: JSON.stringify(updateTask),
    });

    const data = res.json();

    setTasks(
      tasks.map((task) => {
        return task.id === id ? { ...tasks, reminder: data.reminder } : task;
      })
    );
  };

  const addTask = async (task) => {
      const res = await fetch(`http://localhost:5278/tasks`, {
      method: "POST",
      headers: {
        "Content-type": "application/json",
      },
      body: JSON.stringify(task),
    });

      const data = await res.json();
      console.log(data);

    setTasks([...tasks, data]);

    // const id = Math.floor(Math.random() * 1000) + 1;
    // const newTask = { id, ...task };
    // setTasks([...tasks, newTask]);
  };

  useEffect(() => {
    const getTasks = async () => {
      const tasksFromServer = await fetchTasks();
      setTasks(tasksFromServer);
    };

    getTasks();
  }, []);

  const fetchTasks = async () => {
      const res = await fetch("http://localhost:5278/tasks");
    const data = await res.json();

    return data;
  };

  const deleteTask = async (id) => {
      await fetch(`http://localhost:5278/tasks/${id}`, {
      method: "DELETE",
    });

    setTasks(
      tasks.filter((task) => {
        return task.id !== id;
      })
    );
  };

  const fetchTask = async (id) => {
      const res = await fetch(`http://localhost:5278/tasks/${id}`);
    const data = await res.json();

    return data;
  };

  return (
    <div className="container">
      <Header
        title="Task Tracker"
        onAdd={() => {
          return setShowAddTask(!showAddTask);
        }}
        showAddTask={showAddTask}
      />
      {showAddTask && <AddTask onAdd={addTask} />}
      {tasks.length > 0 ? (
        <Tasks tasks={tasks} onDelete={deleteTask} onToggle={toggleReminder} />
      ) : (
        "No Tasks to show"
      )}
      </div>
  );
}

export default App;
