export const environment = {
  production: true,
  apiBase : 'api/v1/',
  azureConfig: {
    //tenant: 'shreedharpatil072gmail.onmicrosoft.com',
    tenant: 'ca07c276-28d9-4b09-86e1-6fadd84de79f',
    clientId: '75a483d2-a35f-4fe6-8c07-fa48ef6bc5b7',
    //resource:'fdb6b9cb-3378-4bc0-b08f-3e0377af9eff',
    //postLogoutRedirectUri: 'https://aemonweb.azurewebsites.net/',
    //redirectUri: 'https://aemonweb.azurewebsites.net/viewusers',
    postLogoutRedirectUri: 'http://localhost:65148',
    redirectUri: 'http://localhost:65148/viewusers'
  }
};
