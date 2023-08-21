FROM mcr.microsoft.com/windows/servercore/iis:windowsservercore-ltsc2019
SHELL ["powershell", "-command"]

RUN Remove-Item -Recurse C:\inetpub\wwwroot\*

RUN Install-WindowsFeature -Name Web-ASP; Install-WindowsFeature -Name Web-ISAPI-Ext

EXPOSE 80


RUN Remove-Website -Name 'Default Web Site'; \
    New-Item -ItemType Directory -Path 'c:\simplecontactsapp'; \
    New-IISSite -Name "simplecontactsapp" \
                -PhysicalPath 'c:\simplecontactsapp' \
                -BindingInformation "*:80:"


ADD . c:\simplecontactsapp