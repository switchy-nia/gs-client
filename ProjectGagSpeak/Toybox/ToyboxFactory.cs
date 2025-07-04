using Buttplug.Client;
using GagSpeak.State.Models;

namespace GagSpeak.Toybox;

// we need a factory to create new instances of Device objects whenever a device is added.
public class ToyboxFactory
{
    private readonly ILoggerFactory _loggerFactory;
    public ToyboxFactory(ILoggerFactory loggerFactory)
    {
        _loggerFactory = loggerFactory;
    }

    public ButtPlugDevice CreateConnectedDevice(ButtplugClientDevice newDevice)
    {
        return new ButtPlugDevice(_loggerFactory.CreateLogger<ButtPlugDevice>(), newDevice);
    }
}
