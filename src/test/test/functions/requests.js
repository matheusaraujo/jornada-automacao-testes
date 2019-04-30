import 'whatwg-fetch'

const request = (url, method, data) => fetch(url, {
  headers: { 'Content-Type': 'application/json' },
  method: method,
  body: data && (method == 'POST' || method == 'PUT') ? JSON.stringify(data) : null
}).then(async function (response) {
  const body = response.status != 204 ? await response.json() : null;
  const status = response.status;
  return {
    status,
    body
  };
}).catch(function (error) {
  return error;
});

export const post = (url, data) => request(url, 'POST', data);
export const put = (url, data) => request(url, 'PUT', data);
export const get = (url) => request(url, 'GET');
export const del = (url) => request(url, 'DELETE');
export const patch = (url) => request(url, 'PATCH');