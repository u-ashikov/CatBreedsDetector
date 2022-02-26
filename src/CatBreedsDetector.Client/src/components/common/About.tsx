import React from "react";

export default class About extends React.Component {
  public render(): JSX.Element {
    return (
      <div>
        <p className="text-left">
          Cat Breeds Detector is a simple application built just for fun, which
          detects (currently) the most common cat breeds based on the uploaded
          picture.
        </p>
        <p className="text-left">
          The Cat Breeds Detector application uses pre-trained machine learning
          model for predicting the cat's breed. The model is trained on over
          2,000 cat images and can recognize: Abyssinian, Bengal, Birman,
          Bombay, British Shorthair, Egyptian Mau, Maine Coon, Persian, Ragdoll,
          Russian blue, Siamese and Sphynx cats.
        </p>
        <p className="text-left">
          For more information about the cats' breeds, you can visit the
          following link:{" "}
          <a href="https://www.petfinder.com/cat-breeds/" target="_blank">
            Cat Breeds
          </a>
        </p>
      </div>
    );
  }
}
