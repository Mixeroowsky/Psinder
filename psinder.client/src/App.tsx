import Home from "./Components/Pages/Nav/Home";
import "./App.css";
import Navbar from "./Components/Pages/Nav/Navbar";
import Auth, { AuthProvider } from "./Helpers/Auth";
import { BrowserRouter as Router, Route, Routes } from "react-router-dom";
import Login from "./Components/Pages/Login";
import Register from "./Components/Pages/Register";
import AddPet from "./Components/Pages/Pet/AddPet";
import AddShelter from "./Components/Pages/Shelter/AddShelter";
import Pets from "./Components/Pages/Pet/Pets";
import About from "./Components/Pages/Nav/About";
import EditShelter from "./Components/Pages/Shelter/EditShelter";
import EditPet from "./Components/Pages/Pet/EditPet";
import CheckShelterProvider from "./Helpers/CheckShelter";

const App = () => {
  return (
    <AuthProvider>
      <Router>
        <CheckShelterProvider>
          <Navbar />
        </CheckShelterProvider>
        <Routes>
          <Route element={<Auth />}>
            <Route path="/" element={<Home />} />
            <Route path="/addShelter" element={<AddShelter />} />
            <Route path="/editShelter" element={<EditShelter />} />
            <Route path="/addPet" element={<AddPet />} />
            <Route path="/editPet" element={<EditPet />} />
          </Route>
          <Route path="/login" element={<Login />} />
          <Route path="/register" element={<Register />} />

          <Route path="/pets" element={<Pets />} />
          <Route path="/about" element={<About />} />
        </Routes>
      </Router>
    </AuthProvider>
  );
};

export default App;
