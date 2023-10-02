import './App.css';
import { Component } from 'react';

function getStatusLabel(status) {
  switch (status) {
    case 0:
      return 'Создан';
    case 1:
      return 'В работе';
    case 2:
      return 'Завершен';
    default:
      return 'Неизвестный';
  }
}

class App extends Component{
  constructor(){
    super()
    this.state = {
      tasks: [],
      newTask: {
        "title": '',
        "description": '',
        "due_Time": '',
        "create_Time": '',
        "status": 0,
      },
      editingTask: null,
      editTitle: '',
      editDescription: '',
      editdue_Time: '',
      editcreate_Time: '',
      editStatus: 0,
    }
  }
  getTasks = async () => {
    try {
      const response = await fetch(
        'https://localhost:5255/api/ToDo',
        {
          method: 'get'
        }
      );
      if (!response.ok){
        throw new Error("Ошибка получения данных");
      }

      const responsejson = await response.json();
      this.setState({
        tasks: responsejson
      });
    } catch (error){
      console.error(error);
    }
  };

  componentDidMount() {
    this.getTasks();
  }

  handleInputChange = (event) => {
    const { name, value } = event.target;
    this.setState((prevState) => ({
      newTask: {
        ...prevState.newTask,
        [name]: value,
      },
    }));
  };

  createTask = async () => {
    try {
      const currentTime = new Date().toISOString();
      const newTaskWithTime = {
        ...this.state.newTask,
        create_Time: currentTime,
      };
      const response = await fetch('https://localhost:5255/api/ToDo', {
        method: 'post',
        headers: {
          'Content-Type': 'application/json',
        },
        body: JSON.stringify(newTaskWithTime),
      });

      if (!response.ok) {
        throw new Error('Ошибка при создании задачи');
      }
      
      this.getTasks();
      this.setState({
        newTask: {
          "title": '',
          "description": '',
          "due_Time": '',
          "status": 0,
        },
      });
    } catch (error) {
      console.error(error);
    }
  };
  setEditingTask = (taskId) => {
    const taskToEdit = this.state.tasks.find((task) => task.id === taskId);
    if (taskToEdit) {
      this.setState({
        editingTask: taskToEdit,
        editTitle: taskToEdit.title,
        editDescription: taskToEdit.description,
        editdue_Time: taskToEdit.due_Time,
        editcreate_Time: taskToEdit.create_Time,
        editStatus: parseInt(taskToEdit.status),
      });
    }
  };

  handleEditInputChange = (event) => {
    const { name, value } = event.target;
    this.setState({
      [name]: name === 'editcreate_Time' ? value : value,
    });
  };


  updateTask = async () => {
    try {
      const { editingTask, editTitle, editDescription, editdue_Time, editcreate_Time, editStatus } = this.state;
      if (!editingTask) return;

      const updatedTask = {
        ...editingTask,
        "title": editTitle,
        "description": editDescription,
        "due_Time": editdue_Time,
        "create_Time": editcreate_Time,
        "status": parseInt(editStatus),
      };

      const response = await fetch(
        `https://localhost:44377/api/ToDo/${editingTask.id}`,
        {
          method: 'put',
          headers: {
            'Content-Type': 'application/json',
          },
          body: JSON.stringify(updatedTask),
        }
      );

      if (!response.ok) {
        throw new Error('Ошибка при обновлении задачи');
      }

      this.getTasks();
      this.setState({
        editingTask: null,
      });
    } catch (error) {
      console.error(error);
    }
  };
  


  deleteTask = async (id) => {
    try {
      const response = await fetch(`https://localhost:5255/api/ToDo/${id}`, {
        method: 'delete',
      });

      if (!response.ok) {
        throw new Error('Ошибка при удалении задачи');
      }

      this.getTasks();
    } catch (error) {
      console.error(error);
    }
  };


  render(){
    const { newTask, tasks, editingTask, editTitle, editDescription, editdue_Time, editStatus } = this.state;

    const taskItems = tasks.map((task) => (
      <div key={task.id}>
        <h3>{task.title}</h3>
        <p>{task.description}</p>
        <p>Выполнить до: {new Date(task.due_Time).toLocaleDateString()}</p>
        <p>Статус: {getStatusLabel(task.status)}</p>
        <button onClick={() => this.deleteTask(task.id)}>Удалить</button>
        <button onClick={() => this.setEditingTask(task.id)}>Редактировать</button>
      </div>
    ));

    return (
      <div>
        <h1>Список задач</h1>
        <div>
          <input
            type="text"
            placeholder="Заголовок"
            name="title"
            value={newTask.title}
            onChange={this.handleInputChange}
          />
          <input
            type="text"
            placeholder="Описание"
            name="description"
            value={newTask.description}
            onChange={this.handleInputChange}
          />
          <input
            type="date"
            placeholder="Выполнить до даты"
            name="due_Time"
            value={newTask.due_Time}
            onChange={this.handleInputChange}
          />
          <button onClick={this.createTask}>Создать задачу</button>
        </div>
        {taskItems}
        {editingTask && (
          <div>
            <h3>Редактировать задачу</h3>
            <input
              type="text"
              placeholder="Заголовок"
              name="editTitle"
              value={editTitle}
              onChange={this.handleEditInputChange}
            />
            <input
              type="text"
              placeholder="Описание"
              name="editDescription"
              value={editDescription}
              onChange={this.handleEditInputChange}
            />
            <input
              type="date"
              placeholder="Выполнить до даты"
              name="editdue_Time"
              value={editdue_Time}
              onChange={this.handleEditInputChange}
            />
            <select
              name="editStatus"
              value={editStatus}
              onChange={this.handleEditInputChange}
            >
              <option value={0}>Создан</option>
              <option value={1}>В работе</option>
              <option value={2}>Завершен</option>
            </select>
            <button onClick={this.updateTask}>Сохранить</button>
          </div>
        )}
      </div>
    );
  }
}
export default App;