## Run Blazor-based .NET Web applications on AWS Serverless

Blazor WebAssembly is a new client-side web development framework that lets developers to use C# to create application frontend. Blazor can run client-side C# code directly in the browser, using WebAssembly. Blazor WebAssembly runs on .NET Core and it is an open source and cross-platform web framework for building single-page application using .NET and C# instead of traditional JavaScript. In this framework you implement Blazor UI components using .NET code and Razor syntax.

Web application developed using Blazor components can be hosted in two different ways. Firstly, Blazor can run directly in the client browser using WebAssembly and alternatively it can also run your client code on the server, where client event communicates to server using SignalR. In this post, you will explore the former approach. You will develop a web application interface using Blazor WebAssembly which will run in the browser and deploy the same as static web site without any .NET server components. This blog for ASP.NET C# developers and shows
steps to host a single page web application using Blazor WebAssembly to AWS, easily.

### Overview of solution

In this post you will create a AWS serverless web application that will allow you to download and save any YouTube videos to your Amazon Simple Storage Service (Amazon S3) bucket. First you will set up the backend layer using Amazon API Gateway and AWS Lambda. We will use .Net Core 3.1 runtime to host AWS Lambda Code. Then you will create a web application frontend, developed using Blazor WebAssembly which will be hosted as a static website with Amazon S3. This website will be accessed via Amazon CloudFront, which is a fast content delivery network (CDN) service that securely delivers static content. We will use Amazon API Gateway to post user request from the front end to the back-end layer.

## Security

See [CONTRIBUTING](CONTRIBUTING.md#security-issue-notifications) for more information.

## License

This library is licensed under the MIT-0 License. See the LICENSE file.

