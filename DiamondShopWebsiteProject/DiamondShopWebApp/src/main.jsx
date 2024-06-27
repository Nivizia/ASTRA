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
      // Choose Diamond then Product
      {
        path: "/diamond/:diamondId/choose-ring/",
        element: <RingList />,
      },
      {
        path: "/diamond/:diamondId/choose-ring/:ringId",
        element: <RingDetails />,
      },
      {
        path: "/diamond/:diamondId/choose-pendant/",
        element: <PendantList />,
      },
      {
        path: "/diamond/:diamondId/choose-pendant/:pendantId",
        element: <PendantDetails />,
      },
      // Choose Product then Diamond
      {
        path: "/ring/",
        element: <RingList />,
      },
      {
        path: "/ring/:ringId",
        element: <RingDetails />,
      },
      {
        path: "/pendant/",
        element: <PendantList />,
      },
      {
        path: "/pendant/:pendantId",
        element: <PendantDetails />,
      },
      {
        path: "/ring/:ringId/choose-diamond/",
        element: <DiamondList />,
      },
      {
        path: "/ring/:ringId/choose-diamond/:diamondId",
        element: <DiamondDetails />,
      },
      {
        path: "/pendant/:pendantId/choose-diamond/",
        element: <DiamondList />,
      },
      {
        path: "/pendant/:pendantId/choose-diamond/:diamondId",
        element: <DiamondDetails />,
      },
      // Cart
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