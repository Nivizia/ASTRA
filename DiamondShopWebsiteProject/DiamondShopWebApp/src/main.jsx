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

import LoginPage from './components/loginPage';
import SignUp from './components/signUp/signUp';

import DiamondList from './components/diamondStuff/diamondlist';
import DiamondDetails from './components/diamondStuff/diamonddetails';

import RingList from './components/ringcomponents/ringlist';
import RingDetails from './components/ringcomponents/ringdetails';

import PendantList from './components/pendantcomponents/pendantlist';
import PendantDetails from './components/pendantcomponents/pendantdetails';

import ShoppingCart from './components/cart/shoppingcart';

//Testing components
import PersistentDrawerRight from './components/persistentDrawerRight';

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
        path: "/login/",
        element: <LoginPage />
      },
      {
        path: "/signup/",
        element: <SignUp />
      },
      {
        path: "/diamond/",
        element: <DiamondList />
      },
      {
        path: "/diamond/:diamondId",
        element: <DiamondDetails />,
      },
      {
        path: "/choose-ring",
        element: <RingList />,
      },
      {
        path: "/choose-pendant",
        element: <PendantList />,
      },
      {
        path: "/pairing/ring/:ringId",
        element: <RingDetails />,
      },
      {
        path: "/pairing/pendant/:pendantId",
        element: <PendantDetails />,
      },
      {
        path: "/cart/",
        element: <ShoppingCart />
      }
    ]
  },
]);

ReactDOM.createRoot(document.getElementById("root")).render(
  <React.StrictMode>
      <RouterProvider router={router} />
  </React.StrictMode>
);