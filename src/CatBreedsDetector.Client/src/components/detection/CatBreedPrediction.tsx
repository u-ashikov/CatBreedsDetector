import * as React from "react";
import { ICatBreedDetectionResult } from "../../models/detection/ICatBreedDetectionResult";

interface ICatBreedPredictionProps {
  prediction: ICatBreedDetectionResult;
}

export default class CatBreedPrediction extends React.Component<ICatBreedPredictionProps> {
  public render(): JSX.Element {
    return (
      <>
        <h1>Your cats is: {this.props.prediction.breed}</h1>
        <h2>
          Probability:
          {Math.ceil(this.props.prediction.predictionProbability * 100).toFixed(
            2
          )}
          %
        </h2>
      </>
    );
  }
}
