import React from "react";
import { FaTimes } from "react-icons/fa";

const Task = ({ task, onDelete, onToggle }) => {
    console.log(task);
  return (
    <div
      className={`task ${task.reminder ? "reminder" : ""}`}
      onDoubleClick={() => {
        onToggle(task.id);
      }}
    >
      <h3>
        {task.text}
        <FaTimes
          style={{ color: "red", cursor: "pointer", float: "right" }}
          onClick={onDelete}
        />
      </h3>
      <p>{task.day}</p>
    </div>
  );
};

export default Task;
