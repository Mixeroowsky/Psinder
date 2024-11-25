import Home from "./Components/Pages/Home";
import "./App.css";
import Navbar from "./Components/Nav/Navbar";
import Auth, { AuthProvider } from "./Helpers/Auth";
import { BrowserRouter as Router, Route, Routes } from "react-router-dom";
import Login from "./Components/Pages/Login";
import Register from "./Components/Pages/Register";
import AddPet from "./Components/Pages/Pet/AddPet";
import Pets from "./Components/Pages/Pet/Pets";

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
        </Routes>
      </Router>
    </AuthProvider>
  );
};

export default App;
