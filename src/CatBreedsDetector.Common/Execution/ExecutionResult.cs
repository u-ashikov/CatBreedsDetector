namespace CatBreedsDetector.Common.Execution
{
    using System;
    using System.Collections.Generic;
    using CatBreedsDetector.Common.Extensions;

    /// <summary>
    /// A class representing the result of an executed operation.
    /// </summary>
    public class ExecutionResult
    {
        private readonly IList<string> _errors;

        /// <summary>
        /// Initializes a new instance of the <see cref="ExecutionResult"/> class.
        /// </summary>
        internal ExecutionResult()
        {
            this._errors = new List<string>();
        }

        /// <summary>
        /// Gets a value indicating whether the <see cref="ExecutionResult"/> is successful.
        /// </summary>
        public bool IsSuccessful => this._errors.Count == default;

        /// <summary>
        /// Gets a collection of all generated errors for the operation.
        /// </summary>
        public IEnumerable<string> Errors => this._errors.AsReadOnly();

        /// <summary>
        /// Use this method to generate a success execution result.
        /// </summary>
        /// <returns>A new instance of execution result with success state.</returns>
        public static ExecutionResult Success() => new ();

        /// <summary>
        /// Use this method to generate an instance of the <see cref="ExecutionResult"/> with unsuccessful state.
        /// </summary>
        /// <param name="errors">A collection of error messages that should be appended to the <see cref="ExecutionResult"/>.</param>
        /// <returns>An instance of the <see cref="ExecutionResult"/>.</returns>
        public static ExecutionResult Fail(string[] errors)
        {
            if (errors.IsNullOrEmpty())
                throw new InvalidOperationException(Constants.Message.ErrorsCollectionIsNotValid);

            var executionResult = new ExecutionResult();
            executionResult.AppendErrors(errors);

            return executionResult;
        }

        /// <summary>
        /// Use this method to generate an instance of the <see cref="ExecutionResult"/> with unsuccessful state.
        /// </summary>
        /// <param name="error">The error message that should be appended to the <see cref="ExecutionResult"/>.</param>
        /// <returns>An instance of the <see cref="ExecutionResult"/>.</returns>
        public static ExecutionResult Fail(string error) => Fail(error.AsArray());

        /// <summary>
        /// Use this method to append error messages to the error's collection.
        /// </summary>
        /// <param name="errors">The error messages that should be appended.</param>
        protected void AppendErrors(params string[] errors)
        {
            foreach (var error in errors)
            {
                if (string.IsNullOrWhiteSpace(error))
                    throw new InvalidOperationException(Constants.Message.ErrorsCollectionIsNotValid);

                this._errors.Add(error);
            }
        }
    }

    /// <summary>
    /// A class representing the result of an executed operation containing an outcome result.
    /// </summary>
    /// <typeparam name="T">The type of the outcome result.</typeparam>
    public class ExecutionResult<T> : ExecutionResult
        where T : class
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ExecutionResult{T}"/> class.
        /// </summary>
        /// <param name="outcome">The outcome of the execution operation.</param>
        private ExecutionResult(T outcome)
        {
            ArgumentNullException.ThrowIfNull(outcome);
            this.Outcome = outcome;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ExecutionResult{T}"/> class.
        /// </summary>
        private ExecutionResult()
        {
        }

        /// <summary>
        /// Gets the outcome of the execution operation.
        /// </summary>
        public T Outcome { get; }

        /// <summary>
        /// Use this method to generate an instance of the <see cref="ExecutionResult{T}"/> with unsuccessful state.
        /// </summary>
        /// <param name="errors">A collection of error messages that should be appended to the <see cref="ExecutionResult{T}"/>.</param>
        /// <returns>An instance of the <see cref="ExecutionResult{T}"/>.</returns>
        public new static ExecutionResult<T> Fail(string[] errors)
        {
            if (errors.IsNullOrEmpty())
                throw new InvalidOperationException(Constants.Message.ErrorsCollectionIsNotValid);

            var executionResult = new ExecutionResult<T>();
            executionResult.AppendErrors(errors);

            return executionResult;
        }

        /// <summary>
        /// Use this method to generate an instance of the <see cref="ExecutionResult{T}"/> with unsuccessful state.
        /// </summary>
        /// <param name="error">The error message that should be appended to the <see cref="ExecutionResult{T}"/>.</param>
        /// <returns>An instance of the <see cref="ExecutionResult"/>.</returns>
        public new static ExecutionResult<T> Fail(string error) => Fail(error.AsArray());

        /// <summary>
        /// Use this method to generate an instance of the <see cref="ExecutionResult{T}"/> with outcome data within it.
        /// </summary>
        /// <param name="outcome">The outcome data that should be set to the <see cref="ExecutionResult{T}"/>.</param>
        /// <returns>An instance of the <see cref="ExecutionResult{T}"/> with outcome data within it.</returns>
        public static ExecutionResult<T> SuccessWith(T outcome) => new (outcome);
    }
}