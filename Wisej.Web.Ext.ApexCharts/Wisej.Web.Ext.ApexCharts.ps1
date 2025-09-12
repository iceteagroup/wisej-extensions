param($installPath, $toolsPath, $package, $project)

$file = "Wisej.Web.Ext.ApexCharts.json"

$contentPath = "$installPath\files\$file"
$projectItems = $project.ProjectItems

foreach($item in $projectItems) {
	if($item.Name -eq $file) {
		exit
	}
}

$projectItems.AddFromFileCopy($contentPath)