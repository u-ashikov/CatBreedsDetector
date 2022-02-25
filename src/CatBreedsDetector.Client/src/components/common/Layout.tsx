import React from "react";
import Footer from "./Footer";
import NavBar from "../navigation/NavBar";
import NavItem from "../navigation/NavItem";
import DetectCatBreedForm from "../detection/DetectCatBreedForm";
import About from "./About";
import "../../styles/main.css";

interface ILayoutState {
  displayDetectBreedForm: boolean;
  displayAbout: boolean;
}

export default class Layout extends React.Component<{}, ILayoutState> {
  public constructor(props: any) {
    super(props);

    this.state = {
      displayDetectBreedForm: false,
      displayAbout: true,
    };
  }

  public render(): JSX.Element {
    return (
      <div id="app" className="container">
        <img
          className="img-thumbnail rounded-circle mt-3"
          src="/cat-paw-logo.png"
        />
        <h1 className="display-3 mt-3">Cat Breeds Detector</h1>
        <hr />
        <div className="row">
          <NavBar>
            <NavItem
              linkText="Detect Breed"
              onClick={() => this.shouldDisplayDetectBreedForm()}
            />
            <NavItem
              linkText="About"
              onClick={() => this.shouldDisplayAbout()}
            />
          </NavBar>
          <div className="col-9">
            {this.state.displayDetectBreedForm && <DetectCatBreedForm />}
            {this.state.displayAbout && <About />}
          </div>
        </div>
        <Footer />
      </div>
    );
  }

  private shouldDisplayAbout(): void {
    if (this.state.displayAbout) return;

    this.setState({
      displayAbout: !this.state.displayAbout,
      displayDetectBreedForm: false,
    });
  }

  private shouldDisplayDetectBreedForm(): void {
    if (this.state.displayDetectBreedForm) return;

    this.setState({
      displayDetectBreedForm: !this.state.displayDetectBreedForm,
      displayAbout: false,
    });
  }
}
