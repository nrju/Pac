# Pac

The simple web application to serve
[PAC (proxy autoconfiguration)](https://developer.mozilla.org/en-US/docs/Web/HTTP/Guides/Proxy_servers_and_tunneling/Proxy_Auto-Configuration_PAC_file)
files.

## Run in docker container

Create the folder for the PAC file.

```bash
mkdir /path/to/pac -p
```

Create the PAC file `/path/to/pac/default.pac`, e.g.:

```js
var PROXY = "PROXY 192.168.0.1:8888";

// The domains to use direct connection without proxy.
var skipProxyDomains = [
  "contoso.com",
  "example.com"
];

function FindProxyForURL(url, host) {
    for(var i in skipProxyDomains) {
        d = skipProxyDomains[i];
        if(host.endsWith("." + d) || host === d) {
            return DIRECT;
        }
    }

    return PROXY;
}
```

Log in to the GitHub container registry.

```bash
docker login -u nrju -p <PAT> ghcr.io
```

Run the container.

```bash
docker run --name pac -v /path/to/pac:/app/wwwroot \
    -p 8080:8080 -d ghcr.io/nrju/pac:latest
```

Set up you browser to use `http://192.168.0.1:8080` as a proxy autoconfiguration URL
(change the address and port to the actual ones).

## Set up PAC for MW Edge with Group Policy

Download unpack and install the `MicrosoftEdgePolicyTemplates.zip` file.

Open Windows Local Group Policy Editor and set `Administrative Templates` / `Microsoft Edge` / `Proxy
server` / `Proxy settings` to the value:

```json
{ "ProxyMode": "pac_script", "ProxyPacUrl": "http://192.168.0.1:8080", "ProxyPacMandatory": false }
```
