//Import
import React from 'react'
import ReactDOM from 'react-dom/client'
import {
  createBrowserRouter,
  RouterProvider,
} from "react-router-dom";


//Component Imports
import Root, {
  loader as rootLoader,
  action as rootAction,
} from "./routes/root";
import ErrorPage from "./error-page";
import Contact, {
  loader as contactLoader,
} from "./routes/contact";
import EditContract, {
  action as editAction,
}
from './routes/edit';


//Css Imports
import './index.css'

const router = createBrowserRouter([
  {
    path: "/",
    element: <Root />,
    errorElement: <ErrorPage />,
    loader: rootLoader,
    action: rootAction,
    children: [
      {
        path: "contacts/:contactId",
        element: <Contact />,
        errorElement: <ErrorPage />,
        loader: contactLoader,
      },
      {
        path: "contacts/:contactId/edit",
        element: <EditContract />,
        errorElement: <ErrorPage />,
        loader: contactLoader,
        action: editAction,
      }
    ],
  },
  
]);

ReactDOM.createRoot(document.getElementById("root")).render(
  <React.StrictMode>
    <RouterProvider router={router} />
  </React.StrictMode>
);