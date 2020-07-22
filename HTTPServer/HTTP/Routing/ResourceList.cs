using System;
using System.Collections.Generic;
using System.Text;
using WebServer.Exceptions.HTTP;

namespace WebServer.HTTP.Routing
{
    public class ResourceList
    {
        private List<Resource> resources;

        public ResourceList() {
            resources = new List<Resource>();
        }

        //<todo>
        //figure out validation
        //for adding the same resousces
        //</todo>
        public void AddResource(Resource resourceToAdd) {
            foreach (Resource resource in resources) {
                if (resource.GetResourceName().Equals(resourceToAdd.GetResourceName())) {
                    throw new ArgumentException(string.Format("resource already exists {0} is the same as {1}", 
                        resource.GetResourceName(), resourceToAdd.GetResourceName()));
                }
            }
            resources.Add(resourceToAdd);
        }

        public Resource GetResourceByName(string resourceName) {
            foreach(Resource resource in resources) {
                if (resource.GetResourceName().Equals(resourceName))
                {
                    return resource;
                }
            }
            throw new HTTPResourceNotFoundException(resourceName);
        }

        //<todo>
        //add search method
        //for method route
        //</todo>
    }
}
