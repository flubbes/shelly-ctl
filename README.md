# ShellyCtl

This is my superquick implementation of managing the config of shelly devices with a single command and an inventory file.

With an inventory file like below

```yaml
default:
  settings:
    cloud:
      enabled: false

inventory:
  - 192.168.2.6
  - 192.168.2.7
```

you can streamline your shelly device configuration over all devices listed in the inventory section.

## Running from the source folder

### MacOs

`dotnet run --project src/ShellyCtl --runtime osx-x64 --self-contained -- apply -f examples/inventory.yaml`

### Linux

`dotnet run --project src/ShellyCtl -- apply -f examples/inventory.yaml`
