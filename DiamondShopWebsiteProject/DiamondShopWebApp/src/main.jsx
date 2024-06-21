//React Imports
import React from 'react'
import ReactDOM from 'react-dom/client'
import {
  createBrowserRouter,
  RouterProvider,
} from "react-router-dom";

//Component Imports
import Root from './components/root';
import Index from './components/index'
import DiamondList from './components/diamondStuff/diamondlist';
import DiamondDetails from './components/diamondStuff/diamonddetails';
import LoginPage from './components/loginPage';
import SignUp from './components/signUp/signUp';

//Css Imports
import './index.css'

const router = createBrowserRouter([
  {
    path: "/",
    element: <Root />,
    children: [
      {
        index: true,
        element: <Index />,
      },
      {
        path: "/diamond/",
        element: <DiamondList />
      },
      {
        path: '/diamond/:diamondId', // Add the route for diamond details
        element: <DiamondDetails />,
      },
      {
        path: "/login/",
        element: <LoginPage />
      },
      {
        path: "/signup/",
        element: <SignUp />
      }
    ]
  }
]);

ReactDOM.createRoot(document.getElementById("root")).render(
  <React.StrictMode>
      <RouterProvider router={router} />
  </React.StrictMode>
);