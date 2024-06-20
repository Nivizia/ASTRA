import React from "react";
import styled from 'styled-components';
import Header from "./HeaderAndFooter/header";
import Footer from "./HeaderAndFooter/footer";

import { Outlet } from "react-router-dom";

const StyledContainer = styled.div
  `font-family: 'Manrope', sans-serif;`;

const MainContent = styled.main`
  flex-grow: 1;`;

const Root = () => {
  return (
    <StyledContainer>
      <div id="page-renderer">
        <Header />  
        <MainContent>
          <Outlet />
        </MainContent>
        <Footer />
      </div>
    </StyledContainer>


  )
}
export default Root