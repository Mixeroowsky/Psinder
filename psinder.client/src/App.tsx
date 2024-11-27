import Home from "./Components/Pages/Nav/Home";
import "./App.css";
import Navbar from "./Components/Pages/Nav/Navbar";
import Auth, { AuthProvider } from "./Helpers/Auth";
import { BrowserRouter as Router, Route, Routes } from "react-router-dom";
import Login from "./Components/Pages/Login";
import Register from "./Components/Pages/Register";
import AddPet from "./Components/Pages/Pet/AddPet";
import Pets from "./Components/Pages/Pet/Pets";
import About from "./Components/Pages/Nav/About";

const App = () => {
  return (
    <AuthProvider>
      <Router>
        <Navbar />
        <Routes>
          <Route element={<Auth />}>
            <Route path="/" element={<Home />} />
          </Route>
          <Route path="/login" element={<Login />} />
          <Route path="/register" element={<Register />} />
          <Route path="/addPet" element={<AddPet />} />
          <Route path="/pets" element={<Pets />} />
          <Route path="/about" element={<About />} />
        </Routes>
      </Router>
    </AuthProvider>
  );
};

export default App;
