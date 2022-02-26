import React from "react";
import "styles/footer.css";

export default class Footer extends React.Component {
  public render(): JSX.Element {
    return (
      <footer id="footer" className="container text-center">
        <hr />
        Copyright &copy; Cat Breeds Detector, developed by YA Software
      </footer>
    );
  }
}
