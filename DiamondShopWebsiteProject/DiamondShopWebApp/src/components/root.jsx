import Header from "./header"
import Footer from "./footer"
import styled from 'styled-components';

const StyledContainer = styled.div
  `font-family: 'Manrope', sans-serif;`;

const Root = () => {
  return (
    <StyledContainer>
      <Header />
      <main>
        <div>
          haro
        </div>
      </main>
      <Footer />
    </StyledContainer>
  )
}
export default Root