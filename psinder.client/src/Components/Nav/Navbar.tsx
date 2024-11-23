import { Button } from "../ui/button";
import { useAuth } from "../../Helpers/Auth";
import { Switch } from "../ui/switch";
import { Label } from "../ui/label";
import { useState, useEffect } from "react";
import { Link, useNavigate } from "react-router-dom";
import "../../App.css";
import logo from "../../assets/logo.png";

const Navbar = () => {
  const [darkMode, setDarkMode] = useState(true);
  const { isAuthenticated, logout } = useAuth();
  const navigate = useNavigate();

  const handleLogout = async () => {
    await logout();
    navigate("/");
  };

  useEffect(() => {
    darkMode
      ? document.documentElement.classList.add("dark")
      : document.documentElement.classList.remove("dark");
  }, [darkMode]);

  const toggleDarkMode = () => setDarkMode(!darkMode);

  return (
    <div>
      <header>
        <div className="container">
          <nav
            className={
              darkMode
                ? "bg-gradient-to-b from-green-900 from-0% to-green-950 to-100%"
                : "bg-gradient-to-b from-green-500 from-0% to-green-700 to-100%"
            }
          >
            <div className="flex gap-4">
              <a href="/">
                <img src={logo} width="64" height="64" alt="logo"></img>
              </a>
              <div className="grid grid-cols-[100px_1fr] flex">
                <h1 className="mt-1">
                  <Link to="/">Psinder</Link>
                </h1>
                <ul>
                  <li>
                    <Link to="/">home</Link>
                  </li>
                  <li>
                    <Link to="/about">about</Link>
                  </li>
                </ul>
              </div>
            </div>

            <div className="flex flex-box">
              <div className="mt-6 space-x-5">
                {isAuthenticated ? (
                  <Button
                    className={darkMode ? "bg-green-600" : "bg-green-400"}
                    onClick={handleLogout}
                  >
                    Log out
                  </Button>
                ) : (
                  <>
                    <Link to="/login">
                      <Button
                        className={darkMode ? "bg-green-600" : "bg-green-400"}
                      >
                        Log In
                      </Button>
                    </Link>
                    <Link to="/register">
                      <Button
                        className={darkMode ? "bg-green-600" : "bg-green-400"}
                      >
                        Register
                      </Button>
                    </Link>
                  </>
                )}
              </div>
              <div className="w-[100px] flex flex-col items-center space-y-2 m-4 pt-1">
                <Switch
                  className="data-[state=unchecked]:bg-green-800 data-[state=checked]:bg-green-500"
                  onCheckedChange={toggleDarkMode}
                />
                <Label htmlFor="dark-mode">
                  {darkMode ? "Light mode" : "Dark mode"}
                </Label>
              </div>
            </div>
          </nav>
        </div>
      </header>
    </div>
  );
};

export default Navbar;
