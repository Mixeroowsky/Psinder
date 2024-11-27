import { Button } from "../../ui/button";
import { useAuth } from "../../../Helpers/Auth";
import { Switch } from "../../ui/switch";
import { Label } from "../../ui/label";
import { useState, useEffect, useContext } from "react";
import { Link, useNavigate } from "react-router-dom";
import "../../../App.css";
import logo from "../../../assets/logo.png";
import { ShelterContext } from "../../../Helpers/CheckShelter";

const Navbar = () => {
  const [darkMode, setDarkMode] = useState(true);
  const { isAuthenticated, logout } = useAuth();
  const navigate = useNavigate();
  const shelterContext = useContext(ShelterContext);
  const { shelterRegistered } = shelterContext;

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
            className={`${
              darkMode
                ? "bg-gradient-to-b from-green-900 from-0% to-green-950 to-100%"
                : "bg-gradient-to-b from-green-500 from-0% to-green-700 to-100%"
            } max-h-20`}
          >
            <div className="flex gap-4">
              <Link to="/">
                <img
                  className="mt-3 ml-2"
                  src={logo}
                  width="48"
                  height="64"
                  alt="logo"
                ></img>
              </Link>
              <div className="grid grid-cols-[100px_1fr] flex">
                <h1 className="mt-0.5 translate-y-2">
                  <Link to="/">Psinder</Link>
                </h1>
                <ul>
                  <li>
                    <Link to="/">Home</Link>
                  </li>

                  <li>
                    <Link to="/pets">All pets</Link>
                  </li>
                  <li>
                    <Link to="/about">About</Link>
                  </li>
                </ul>
              </div>
            </div>

            <div className="flex flex-box">
              <div className="mt-4 space-x-5">
                {isAuthenticated ? (
                  <>
                    {shelterRegistered ? (
                      <Link to="/editShelter">
                        <Button
                          className={`${
                            darkMode ? "bg-green-600" : "bg-green-400"
                          } text-lg w-36 h-10 `}
                        >
                          View your shelter
                        </Button>
                      </Link>
                    ) : (
                      <Link to="/addShelter">
                        <Button
                          className={`${
                            darkMode ? "bg-green-600" : "bg-green-400"
                          } text-lg w-36 h-10 `}
                        >
                          Register a shelter
                        </Button>
                      </Link>
                    )}
                    <Button
                      className={`${
                        darkMode ? "bg-green-600" : "bg-green-400"
                      } text-lg w-24 h-10 `}
                      onClick={handleLogout}
                    >
                      Log out
                    </Button>
                  </>
                ) : (
                  <>
                    <Link to="/login">
                      <Button
                        className={`${
                          darkMode ? "bg-green-600" : "bg-green-400"
                        } text-lg w-24 h-10 `}
                      >
                        Log In
                      </Button>
                    </Link>
                    <Link to="/register">
                      <Button
                        className={`${
                          darkMode ? "bg-green-600" : "bg-green-400"
                        } text-lg w-24 h-10 `}
                      >
                        Sign up
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
