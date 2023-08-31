# Umbraco.Headless.Blazor 🤯

## About👋

This is a sample Blazor WASM (WebAssembly) application that makes use of the new Umbraco 12 Content Delivery API to display content.

It was built as a POC/demo to test the capabilities of the new API, and make it easy to play around and see the requests in the browser.

- `UmbracoDemo.Client` - The Blazor WASM (front-end) application.
	- Added the Umbraco Content Delivery API as an OpenApiReference, to auto-generate the client code.
- `UmbracoDemo.CMS` - A sample Umbraco 12 CMS application with the Content Delivery API enabled.
	- `Custom` folder - Includes the custom code built to extend the Content Delivery API.

## Features✨

- [x] Sample pages
- [x] Blocks (using block list)
- [x] Content overview
	- [x] Custom sort
	- [ ] Custom filter (tags?)
- [x] Multi-lingual
- [x] Preview mode
- [x] Simple search
	- [x] Custom search filter
	- [x] Pagination
	- [x] Hide certain pages from search
- [ ] Custom property editors (use a community package)
- [ ] Extend the API response
	- [ ] Add my own property editor
	- [ ] Use code
	
## Try it out🙌

### Demo🌐

https://lauraneto.github.io/Umbraco.Headless.Blazor/

### Locally💻

You can try out the sample applications by following these steps:

1. Clone the repository.
2. Open a terminal in the root of the repository.
3. Start the CMS application by running the following command:

	```bash
	dotnet run --project .\src\UmbracoDemo.CMS\UmbracoDemo.CMS.csproj
	```

	**CMS credentials**   
	Email: `test@test.com`  
	Password: `NotASecret123!`

4. Start the Blazor WASM application by running the following command:
	```bash
	dotnet run --project .\src\UmbracoDemo.Client\UmbracoDemo.Client.csproj
	```

5. Open a browser and navigate to `https://localhost:5001`.
